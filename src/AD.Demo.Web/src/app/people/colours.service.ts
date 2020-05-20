import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Colour, ColourStats } from './person..model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ColoursService {

  constructor(private _httpClient: HttpClient) { }

  public getAll(): Observable<Colour[]> {
    return this._httpClient.get<Colour[]>(`${environment.baseUrl}/colour`);
  }

  public getStats(): Observable<ColourStats[]> {
    return this._httpClient.get<ColourStats[]>(`${environment.baseUrl}/colour/stats`);
  }
}
