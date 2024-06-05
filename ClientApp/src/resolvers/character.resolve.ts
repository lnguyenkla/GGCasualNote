import {ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot} from "@angular/router";
import {Character} from "../models";
import {CharacterService} from "../services/character.service";
import {inject} from "@angular/core";
import {of} from "rxjs";

export const characterResolve: ResolveFn<Character[]> =
  (route: ActivatedRouteSnapshot,  state: RouterStateSnapshot) => {
    const characterService: CharacterService = inject(CharacterService);
    console.log(characterService);

    if (characterService.availableCharacters.length === 0) {
      return inject(CharacterService).getCharacters();
    }

    return of(characterService.availableCharacters);
  };
