import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Colour } from './person..model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ColoursService {

  constructor(private _httpClient: HttpClient) { }

  public getAll(): Observable<Colour[]> {
    return this._httpClient.get<Colour[]>(`${environment.baseUrl}/colour`);
  }
}
