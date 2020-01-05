import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { LotListComponent } from './components/lot-list/lot-list.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { SignInGuard } from './guards/sign-in.guard';
import { UnauthorizedGuard } from './guards/unauthorized.guard';
import { LotComponent } from './components/lot/lot.component';
import { AdminComponent } from './components/admin/admin.component';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import { CreateLotComponent } from './components/create-lot/create-lot.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { BidListComponent } from './components/bid-list/bid-list.component';
import { EditLotComponent } from './components/edit-lot/edit-lot.component';
import { UsersComponent } from './components/admin/users/users.component';
import { LotsComponent } from './components/admin/lots/lots.component';
import { BidsComponent } from './components/admin/bids/bids.component';



const routes: Routes = [
  { path: 'lots/:id/edit', component: EditLotComponent, canActivate: [SignInGuard]},
  { path: 'users/profile/bids', component: BidListComponent, canActivate: [SignInGuard]},
  { path: 'users/profile', component: UserProfileComponent, canActivate: [SignInGuard]},
  { 
    path: 'admin', component: AdminComponent, canActivate: [SignInGuard], data : {permittedRoles : ['Admin', 'Moderator']},
    children: [
      { path: 'users', component: UsersComponent},
      { path: 'lots', component: LotsComponent},
      { path: 'bids', component: BidsComponent}
    ]
  },
  { path: 'forbidden', component: ForbiddenComponent},
  { path: 'signin', component: SignInComponent, canActivate: [UnauthorizedGuard]},
  { path: 'signup', component: SignUpComponent, canActivate: [UnauthorizedGuard]},
  { path: 'categories', component: CategoryListComponent},
  { path: 'users/profile/lots', component: LotListComponent, canActivate: [SignInGuard]},
  { path: 'categories/:id/lots', component: LotListComponent},
  { path: 'lots', component: LotListComponent},
  { path: 'createLot', component: CreateLotComponent, canActivate: [SignInGuard]},
  { path: 'lots/:id', component: LotComponent},
  { path: '', component: CategoryListComponent, pathMatch: 'full' },
  { path: '**', redirectTo: 'categories' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [
  UserProfileComponent, 
  CreateLotComponent, 
  AdminComponent, 
  CategoryListComponent, 
  LotListComponent, 
  LotComponent, 
  SignInComponent,
  SignUpComponent,
  BidListComponent,
  EditLotComponent,
  ForbiddenComponent]
