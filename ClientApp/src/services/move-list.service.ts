import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MoveListService {
  public lastUpdatedTimestamp?: Date;

  constructor(private http: HttpClient) { }

  public getMoveList(characterId: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('characterId', characterId);

    return this.http.get('https://localhost:44480/api/move', {
      params: params
    });
  }

  public getLastUpdatedTimestamp(characterId: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('characterId', characterId);

    return this.http.get('https://localhost:44480/api/movelisttimestamp', {
      params: params
    });
  }

  public putScrapMoveList(characterId: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('characterId', characterId);

    return this.http.put('https://localhost:44480/api/move', null, {
      params: params
    });
  }
}
