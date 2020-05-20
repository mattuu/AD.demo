import { Component, OnInit } from '@angular/core';
import { ColoursService } from '../colours.service';
import { Observable } from 'rxjs';
import { ColourStats } from '../person..model';

@Component({
  selector: 'app-colour-stats',
  templateUrl: './colour-stats.component.html',
  styleUrls: ['./colour-stats.component.css']
})
export class ColourStatsComponent implements OnInit {

  public stats$: Observable<ColourStats[]>;

  constructor(private _colourService: ColoursService) { }

  ngOnInit(): void {
    this.stats$ = this._colourService.getStats();
  }

}
