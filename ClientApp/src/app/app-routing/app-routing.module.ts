import {inject, NgModule} from '@angular/core';
import {Routes, RouterModule, ResolveFn, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {HomeComponent} from "../home/home.component";
import {CharacterSelectComponent} from "../character-select/character-select.component";
import {Character} from "../../models";
import {CharacterService} from "../../services/character.service";
import {of, tap} from "rxjs";

export const characterResolve: ResolveFn<Character[]> =
  (route: ActivatedRouteSnapshot,  state: RouterStateSnapshot) => {
    const characterService: CharacterService = inject(CharacterService);
    console.log(characterService);

    if (characterService.availableCharacters.length === 0) {
      return inject(CharacterService).getCharacters();
    }

    return of(characterService.availableCharacters);
  };

const routes: Routes = [
  {path: 'character-select', component: CharacterSelectComponent, resolve: { characters: characterResolve }},
  {path: '', component: HomeComponent, pathMatch: 'full'}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
