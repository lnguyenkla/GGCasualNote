import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {ComboNote} from "../models";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ComboNoteService {

  constructor(private http: HttpClient) { }

  public getComboNotes(characterId: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('characterId', characterId);

    return this.http.get('https://localhost:44480/ggnote/get-combo-notes', {
      params: params
    });
  }

  public saveComboNote(note: ComboNote): Observable<string> {
    return this.http.post('https://localhost:44480/ggnote/create-combo-note', note, {
      responseType: 'text'
    });
  }

  public updateComboNote(note: ComboNote): Observable<string> {
    return this.http.put('https://localhost:44480/ggnote/update-combo-note', note, {
      responseType: 'text'
    });
  }

  public deleteComboNote(id: number): Observable<string> {
    let params: HttpParams = new HttpParams();
    params = params.append('comboNoteId', id);

    return this.http.delete('https://localhost:44480/ggnote/delete-combo-note', {
      params: params,
      responseType: 'text'
    });
  }
}
