import { Component, OnInit, Input } from '@angular/core';
import { faSquare } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-colour-icon',
  templateUrl: './colour-icon.component.html',
  styleUrls: ['./colour-icon.component.css']
})
export class ColourIconComponent implements OnInit {
  private _colorsDictionary = []

  @Input()
  public set colourName(val: string) {
    this.colour = `#${this._colorsDictionary[val]}`;
  }

  public icon: any;
  public colour: string;

  constructor() {
    this._colorsDictionary['Red'] = 'ff595e';
    this._colorsDictionary['Blue'] = '1982c4';
    this._colorsDictionary['Green'] = '8ac926';

    this.icon = faSquare;
  }

  ngOnInit(): void {
  }
}
