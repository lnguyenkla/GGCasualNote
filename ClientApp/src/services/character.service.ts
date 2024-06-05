import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {Character} from "../models";

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  public availableCharacters: Character[] = [];
  constructor(private http: HttpClient) {}

  public getCharacters(): Observable<any> {
    return this.http.get('https://localhost:44480/api/character').pipe(tap(
      (characters: any) => {
        this.availableCharacters = characters;
        console.log(`tap completed in ${this}`)
      }
    ));
  }
}
