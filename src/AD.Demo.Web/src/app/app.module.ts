import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule }   from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PeopleComponent } from './people/people.component';
import { PersonEditComponent } from './people/person-edit/person-edit.component';
import { ColoursIndicatorComponent } from './people/colours-indicator/colours-indicator.component';

@NgModule({
  declarations: [
    AppComponent,
    PeopleComponent,
    PersonEditComponent,
    ColoursIndicatorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    FontAwesomeModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
