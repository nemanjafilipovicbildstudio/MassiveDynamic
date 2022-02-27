import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from 'src/services/global-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {
  public users: User[];

  constructor(
    private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
    private globalService: GlobalService
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }
    
  private getUsers() {
    this.http.get<User[]>(this.baseUrl + 'users/getall').subscribe(result => {
      this.users = result;
    }, error => {
        console.error(error);
        this.globalService.checkAccessDenied(error);
    });
  }
  
  public delete(id: string){
    if(confirm("Are you sure you want to delete this user?")){
      this.deleteUser(id);
    }
  }

  private deleteUser(id: string){
    this.http.delete(this.baseUrl + 'users?id=' + id).subscribe(result => {
      this.getUsers();
    }, error => {
      console.error(error);
      this.globalService.checkAccessDenied(error);
    });
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
