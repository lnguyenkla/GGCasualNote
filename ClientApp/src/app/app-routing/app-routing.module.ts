import {inject, NgModule} from '@angular/core';
import {Routes, RouterModule, ResolveFn, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {HomeComponent} from "../home/home.component";
import {CharacterSelectComponent} from "../character-select/character-select.component";
import {Character} from "../../models";
import {CharacterService} from "../../services/character.service";

export const characterResolve: ResolveFn<Character[]> =
  (route: ActivatedRouteSnapshot,  state: RouterStateSnapshot) => {
    return inject(CharacterService).getCharacters();
  };

const routes: Routes = [
  {path: 'character-select', component: CharacterSelectComponent, resolve: { characters: characterResolve } },
  {path: '', component: HomeComponent, pathMatch: 'full'}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
