import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PeopleService } from '../people.service';
import { Person, Colour } from '../person..model';
import { ColoursService } from '../colours.service';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent implements OnInit {

  private _routeSubscription: any;
  public id: number;
  public person: Person;
  public colours: Colour[];

  constructor(private _route: ActivatedRoute, 
    private _peopleService: PeopleService, 
    private _coloursService: ColoursService,
    private _router: Router) { }

  ngOnInit(): void {
    this._routeSubscription = this._route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number

      this._peopleService.get(this.id).subscribe(person => {
        this.person = person;
      })

      this._coloursService.getAll().subscribe(colours => {
        this.colours = colours;
      })
    });

  }

  ngOnDestroy() {
    this._routeSubscription.unsubscribe();
  }

  onSubmit() {
    this._peopleService.update(this.id, this.person).subscribe(res => {
      if (res) {
        this._router.navigate(['']);
      }
    })
  }

  public toggleColor(id: number) {
    console.log(id);
    debugger;

    const currentColours = this.person.colours.map(c => c.id);

    if(currentColours.indexOf(id) > 0)
    {
      this.person.colours = this.person.colours.splice(currentColours.indexOf(id));
    }
  }
}
