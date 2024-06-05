import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {MoveListService} from "../../services/move-list.service";
import {MatSelectChange} from "@angular/material/select";
import {DomSanitizer} from "@angular/platform-browser";
import {ComboNoteService} from "../../services/combo-note.service";
import {Router} from "@angular/router";
import {Character} from "../../models";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  public availableCharacters: Character[] = [];

  constructor(private router: Router,
              private characterService: CharacterService) {
  }

  ngOnInit() {
    this.characterService.getCharacters().subscribe((characters: Character[]) => {
        this.availableCharacters = characters;
      })
  }

  public goToCharacterDetail(characterId: string) {
    this.router.navigate(['character-select'], { queryParams: { characterId: characterId } }).then();
  }
}
