import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { GlobalService } from "src/services/global-service";

@Component({
    selector: 'app-companies',
    templateUrl: './companies.component.html'
  })
  export class CompaniesComponent implements OnInit {
    companies: GetCompany[];
    addNewCompany: boolean = false;
    companyForm: FormGroup;
    editCompanyId: string = null;
    userRoles: string[];

    constructor(
      private http: HttpClient, 
      @Inject('BASE_URL') private baseUrl: string, 
      private globalService: GlobalService,
      private formBuilder: FormBuilder
    ) {}
    
    ngOnInit(): void {
        this.userRoles = JSON.parse(sessionStorage.getItem('userRoles')) as string[];
        this.getCompanies();
        this.createForm();
    }
      
    private getCompanies() {
      this.http.get<GetCompany[]>(this.baseUrl + 'company/getall').subscribe(result => {
        this.companies = result;
      }, error => {
          console.error(error);
          this.globalService.checkAccessDenied(error);
      });
    }    

    createForm(){
      this.companyForm = this.formBuilder.group({
        name: ["", Validators.required],
        address: ["", Validators.required],
      });
    }

    get f() { return this.companyForm.controls; }
    
    addNewComapny(){
      this.addNewCompany = true;
      this.cancelEditCompany();
    }

    saveNewCompany(){
      const company: Company = {
        id: null,
        name: this.companyForm.controls['name'].value,
        address: this.companyForm.controls['address'].value
      };

      this.http.post(this.baseUrl + 'company', company)
        .subscribe(res => {
          this.addNewCompany = false;
          this.createForm();
          this.getCompanies();
      }, error => {
        console.error(error);
        this.globalService.checkAccessDenied(error);
      });
    }

    cancelAddingNewComapny(){
      this.addNewCompany = false;
      this.createForm();
    }

    editCompany(id: string) {
      this.editCompanyId = id;
      this.cancelAddingNewComapny();
      const company = this.companies.find(x => x.id == id);
      this.companyForm.controls["name"].setValue(company.name);
      this.companyForm.controls["address"].setValue(company.address);
    }

    saveEditedCompany(){
      const company: Company = {
        id: this.editCompanyId,
        name: this.companyForm.controls['name'].value,
        address: this.companyForm.controls['address'].value
      };

      this.http.put(this.baseUrl + 'company', company)
        .subscribe(res => {
          this.cancelEditCompany();
          this.getCompanies();
      }, error => {
        console.error(error);
        this.globalService.checkAccessDenied(error);
      });
    }

    cancelEditCompany() {
      this.editCompanyId = null;
      this.createForm();
    }
    
    deleteCompany(id: string) {
      if(confirm("Are you sure you want to delete this company?")) {
        this.http.delete(this.baseUrl + 'company?id=' + id).subscribe(result => {
          this.getCompanies();
        }, error => {
          console.error(error);
          this.globalService.checkAccessDenied(error);
        });
      }
    }
    
    isAddButtonEnabled(){
      return this.userRoles.includes('Admin') || this.userRoles.includes('Secretary');
    }

    isEditButtonEnabled(){
      return this.userRoles.includes('Admin') || this.userRoles.includes('Secretary');
    }

    isDeleteButtonEnabled(){
      return this.userRoles.includes('Admin');
    }

  }

  interface Company {
    id: string;
    name: string;
    address: number;
  }

  interface GetCompany {
    id: string;
    name: string;
    address: number;
    contactPersons: ContactPerson[]
  }

  interface ContactPerson {
    id: string;
    firstName: string;
    lastName: number;
    email: string;
    phoneNumber: string;
    address: string;
  }