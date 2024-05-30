import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MoveListService {

  constructor(private http: HttpClient) { }

  public getMoveList(characterId: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('characterId', characterId);

    return this.http.get('https://localhost:44480/ggnote/get-move-list', {
      params: params
    });
  }
}
