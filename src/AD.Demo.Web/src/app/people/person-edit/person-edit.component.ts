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

  constructor(private _route: ActivatedRoute,
    private _peopleService: PeopleService,
    private _coloursService: ColoursService,
    private _router: Router) { }

  ngOnInit(): void {
    this._routeSubscription = this._route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number

      // this._peopleService.get(this.id).subscribe(person => {
      //   this.person = person;
      // })

      const personPromise = this._peopleService.get(this.id);
      const colourPromise = this._coloursService.getAll();


      forkJoin([personPromise, colourPromise]).subscribe({
        next: value => {
          console.log(value);
          // debugger;
          this.person = value[0];

          const usedColours = this.person.colours.map(c => c.id)
          this.colours = value[1].map(c => new Selectable<Colour>(c, usedColours.indexOf(c.id) >= 0));
        },

        // complete: () => {
        //   console.log('This is how it ends!');
        //   debugger;
        // },
      })


      this._coloursService.getAll().subscribe(colours => {
        // const usedColours = this.person.colours.map(c => c.id)

        // const x = colours.map(c => new Selectable<Colour>(c, usedColours.indexOf(c.id) >= 0));
        // this.colours = x;
      })
    });

  }

  ngOnDestroy() {
    this._routeSubscription.unsubscribe();
  }

  onSubmit() {
    const usedColours = this.colours.filter(c => c.selected)
      .map(c => c.item.id);
    
    const model = new UpdatePerson(this.person, usedColours);

    this._peopleService.update(this.id, model).subscribe(res => {
      if (res) {
        this._router.navigate(['']);
      }
    })
  }
}
