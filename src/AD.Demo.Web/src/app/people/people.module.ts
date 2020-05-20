import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PeopleComponent } from './people.component';
import { PersonEditComponent } from './person-edit/person-edit.component';
import { SharedModule } from '../shared/shared.module';
import { ColourStatsComponent } from './colour-stats/colour-stats.component';

@NgModule({
  declarations: [
    PeopleComponent,
    PersonEditComponent,
    ColourStatsComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class PeopleModule { }
