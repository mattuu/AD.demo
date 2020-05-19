import { Component, OnInit } from '@angular/core';
import { PeopleService } from './people.service';
import { Observable } from 'rxjs';
import { Person } from './person..model';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {

  constructor(private _peopleService: PeopleService) { }

  public people$: Observable<Person[]>;

  ngOnInit(): void {
    this.people$ = this._peopleService.getAll();
  }

}
