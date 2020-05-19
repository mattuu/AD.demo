import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SpinnerComponent } from './components';

@NgModule({
  declarations: [SpinnerComponent],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    FontAwesomeModule
  ],
  exports: [
    FormsModule,
    HttpClientModule,
    FontAwesomeModule,
    RouterModule,
    SpinnerComponent
  ]
})
export class SharedModule { }
