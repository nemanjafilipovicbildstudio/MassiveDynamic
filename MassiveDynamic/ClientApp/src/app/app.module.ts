import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { CompaniesComponent } from './companies/companies.component';
import { CompanyDetailsComponent } from './companies/company-details.component';
import { UserDetailsComponent } from './users/user-details.component';
import { UsersComponent } from './users/users.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,    
    UsersComponent,
    UserDetailsComponent,
    AccessDeniedComponent,
    CompaniesComponent,
    CompanyDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-users', component: UsersComponent, canActivate: [AuthorizeGuard] },
      { path: 'user-details/:id', component: UserDetailsComponent, canActivate: [AuthorizeGuard] },
      { path: 'fetch-companies', component: CompaniesComponent, canActivate: [AuthorizeGuard] },
      { path: 'company-details/:id', component: CompanyDetailsComponent, canActivate: [AuthorizeGuard] },
      { path: 'access-denied', component: AccessDeniedComponent, canActivate: [AuthorizeGuard] }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
