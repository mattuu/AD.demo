import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PeopleService } from '../people.service';
import { Person, Colour, ISelectable, Selectable, UpdatePerson } from '../person..model';
import { ColoursService } from '../colours.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent implements OnInit {

  private _routeSubscription: any;
  public id: number;
  public person: Person;
  public colours: ISelectable<Colour>[];

  public busy: boolean = true;
  public heading: string = 'Amend';

  public get isNew(): boolean {
    return isNaN(this.id);
  }

  constructor(private _route: ActivatedRoute,
    private _peopleService: PeopleService,
    private _coloursService: ColoursService,
    private _router: Router) { }

  ngOnInit(): void {
    this._routeSubscription = this._route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number

      const colourPromise = this._coloursService.getAll();

      if (this.isNew) {
        this.heading = 'Enter';

        this.person = new Person();

        colourPromise.subscribe(val => {
          this.colours = val.map(c => new Selectable<Colour>(c, false));
          this.busy = false;
        })

      } else {
        const personPromise = this._peopleService.get(this.id);

        forkJoin([personPromise, colourPromise]).subscribe({
          next: value => {
            this.person = value[0];

            const usedColours = this.person.colours.map(c => c.id)
            this.colours = value[1].map(c => new Selectable<Colour>(c, usedColours.indexOf(c.id) >= 0));

            this.busy = false;
          },
        })
      }
    });

  }

  ngOnDestroy() {
    this._routeSubscription.unsubscribe();
  }

  onSubmit(form) {
    if (form.form.invalid) {
      return;
    }

    const usedColours = this.colours.filter(c => c.selected)
      .map(c => c.item.id);

    const model = new UpdatePerson(this.person, usedColours);

    const callback = (res) => {
      if (res) {
        this._router.navigate(['']);
      }
    };

    if (this.isNew) {
      this._peopleService.create(model).subscribe(callback)
    } else {
      this._peopleService.update(this.id, model).subscribe(callback)
    }
  }

  public deletePerson() {
    if (window.confirm("Are you sure?")) {
      this._peopleService.delete(this.id).subscribe(res => {
        this._router.navigate(['']);
      });
    }
  }
}
