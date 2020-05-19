import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Person } from './person..model';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  constructor(private _httpClient: HttpClient) {
  }

  public getAll(): Observable<Person[]> {
    return this._httpClient.get<Person[]>(`${environment.baseUrl}/person`)
  }

}
