import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class GlobalService {
  constructor(
    private router: Router
  ) { }
  
  checkAccessDenied(error: any) {
    if(error.url.toString().includes("AccessDenied")){
        this.router.navigate(['/access-denied']);
      }
  }
}