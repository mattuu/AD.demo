import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Person, UpdatePerson } from './person..model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  constructor(private _httpClient: HttpClient) {
  }

  public getAll(): Observable<Person[]> {
    return this._httpClient.get<Person[]>(`${environment.baseUrl}/person`, httpOptions)
  }

  public get(id: number): Observable<Person> {
    return this._httpClient.get<Person>(`${environment.baseUrl}/person/${id}`)
  }

  public update(id: number, model: UpdatePerson): Observable<any> {
    return this._httpClient.put(`${environment.baseUrl}/person/${id}`, JSON.stringify(model), httpOptions);
  }

  public delete(id: number): Observable<any> {
    return this._httpClient.delete(`${environment.baseUrl}/person/${id}`, httpOptions);
  }
}
