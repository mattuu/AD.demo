import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PeopleComponent } from './people/people.component';
import { PersonEditComponent } from './people/person-edit/person-edit.component';


const routes: Routes = [
  { path: '', component: PeopleComponent },
  { path: 'new', component: PersonEditComponent },
  { path: 'edit/:id', component: PersonEditComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
