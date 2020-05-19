import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PeopleComponent } from './people.component';
import { PersonEditComponent } from './person-edit/person-edit.component';
import { ColoursIndicatorComponent } from './colours-indicator/colours-indicator.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    PeopleComponent,
    PersonEditComponent,
    ColoursIndicatorComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class PeopleModule { }
