import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray, FormControl  } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from 'rxjs/operators';
import { GlobalService } from "src/services/global-service";

@Component({
    selector: 'app-user-details',
    templateUrl: './user-details.component.html'
  })
  export class UserDetailsComponent implements OnInit {
    form: FormGroup;
    id: string;
    loading = false;
    submitted = false;
    user: User;
    allRoles: Role[] | void;

    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private http: HttpClient, 
      @Inject('BASE_URL') private baseUrl: string,
      private globalService: GlobalService
    ) { }


    async ngOnInit(): Promise<void> {
      this.id = this.route.snapshot.params['id'];
      this.allRoles = await this.http.get<Role[]>(this.baseUrl + "roles").pipe(first()).toPromise().catch(e => this.globalService.checkAccessDenied(e)) as Role[];
      this.user = await this.http.get<User>(this.baseUrl + 'users?id=' + this.id).pipe(first()).toPromise().catch(e => this.globalService.checkAccessDenied(e)) as User;

      if(this.user && this.allRoles){
        this.form = this.formBuilder.group({
          firstName: [this.user.firstName, Validators.required],
          lastName: [this.user.lastName, Validators.required],
          userName: [this.user.userName, Validators.required],
          email: [this.user.email, [Validators.required, Validators.email]],
          roles: this.formBuilder.group({}),
        });
  
        const roles = <FormGroup>this.form.get('roles');
        this.allRoles.forEach((role: Role) => {
          roles.addControl(role.name, new FormControl(this.user.roles.includes(role.name)));
        });
      }
      
    }

    get f() { return this.form.controls; }

    onSubmit() {
      this.submitted = true;
      if (this.form.invalid) {
          return;
      }

      this.user.firstName = this.form.controls['firstName'].value;
      this.user.lastName = this.form.controls['lastName'].value;
      this.user.userName = this.form.controls['userName'].value;
      this.user.email = this.form.controls['email'].value;
      this.user.roles = this.getSelectedRoles();

      this.http.put(this.baseUrl + 'users', this.user)
        .subscribe(res => {
          this.router.navigate(['/fetch-users'], { relativeTo: this.route });
      }, error => {
        console.error(error);
        this.globalService.checkAccessDenied(error);
      });

  }

  private getSelectedRoles(): string[] {
    var roles = this.form.get('roles').value;
    return Object.entries(roles).filter(x => x[1]).map(x => x[0]);
  }


}

  interface User {
    id: string;
    firstName: string;
    lastName: number;
    userName: number;
    email: string;
    roles: string[];
  }

  interface Role {
    name: string;
  }
