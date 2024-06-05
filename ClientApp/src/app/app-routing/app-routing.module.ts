import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {HomeComponent} from "../home/home.component";
import {CharacterSelectComponent} from "../character-select/character-select.component";
import {characterResolve} from "../../resolvers/character.resolve";


const routes: Routes = [
  {path: 'character-select', component: CharacterSelectComponent, resolve: { characters: characterResolve }},
  {path: '', component: HomeComponent, pathMatch: 'full'}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
