import { Component, OnInit, Input } from '@angular/core';
import { Colour } from '../person..model';
import { faSquare } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-colours-indicator',
  templateUrl: './colours-indicator.component.html',
  styleUrls: ['./colours-indicator.component.css']
})
export class ColoursIndicatorComponent implements OnInit {
  private _colorsDictionary = []

  constructor() {
    this._colorsDictionary['Red'] = 'ff595e';
    this._colorsDictionary['Blue'] = '1982c4';
    this._colorsDictionary['Green'] = '8ac926';
  }

  @Input()
  public colours: string[];

  public blueSquare: any;

  ngOnInit(): void {
    this.blueSquare = { ...faSquare, colour: 'blue' };
    this.blueSquare.colour = 'blue';
  };

  public getColour(name: string): string {
    return `#${this._colorsDictionary[name]}`;
  }

}
