import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';


import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { LotListComponent } from './components/lot-list/lot-list.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { TokenInterceptorService } from './services/http-interceptor.service'
import { ReactiveFormsModule } from '@angular/forms'
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';
import { LotComponent } from './components/lot/lot.component';
import { AdminComponent } from './components/admin/admin.component';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import { CreateLotComponent } from './components/create-lot/create-lot.component';

import { DatePickerComponent } from './components/create-lot/date-picker/date-picker.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { BidListComponent } from './components/bid-list/bid-list.component';
import { LotFilterPipe } from './pipes/lot-filter.pipe';
import { EditLotComponent } from './components/edit-lot/edit-lot.component';
import { UsersComponent } from './components/admin/users/users.component';
import { LotsComponent } from './components/admin/lots/lots.component';
import { BidsComponent } from './components/admin/bids/bids.component';
import { EditDateComponent } from './components/edit-lot/edit-date/edit-date.component';
import { CategoriesComponent } from './components/admin/categories/categories.component';
import { OrderModule } from 'ngx-order-pipe';


@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    CategoryListComponent,
    LotListComponent,
    NavbarComponent,
    SignInComponent,
    SignUpComponent,
    LotComponent,
    AdminComponent,
    ForbiddenComponent,
    CreateLotComponent,
    DatePickerComponent,
    UserProfileComponent,
    BidListComponent,
    LotFilterPipe,
    EditLotComponent,
    UsersComponent,
    LotsComponent,
    BidsComponent,
    EditDateComponent,
    CategoriesComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 1000,
      positionClass: 'toast-top-center',
      preventDuplicates: true,
      progressBar: true
    }),
    NgbModule,
    FormsModule,
    OrderModule
  ],
  providers: [AuthService, UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
