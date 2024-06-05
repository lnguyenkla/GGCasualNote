import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {MoveListService} from "../../services/move-list.service";
import {MatSelectChange} from "@angular/material/select";
import {DomSanitizer} from "@angular/platform-browser";
import {ComboNoteService} from "../../services/combo-note.service";
import {ActivatedRoute, Router, UrlSegment} from "@angular/router";
import {Character} from "../../models";
import {HEADER_TIMESTAMP_UPDATE} from "../header/header.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  public availableCharacters: Character[] = [];

  constructor(private router: Router,
              private activatedRoute: ActivatedRoute,
              private characterService: CharacterService) {
  }

  ngOnInit() {
    if (this.characterService.availableCharacters.length > 0) {
      this.availableCharacters = this.characterService.availableCharacters;
    }
    else {
      this.characterService.getCharacters().subscribe((characters: Character[]) => {
        this.availableCharacters = characters;
      });
    }

    this.activatedRoute.url.subscribe((segments: UrlSegment[]) => {
      HEADER_TIMESTAMP_UPDATE.emit('');
    });
  }

  public goToCharacterDetail(characterId: string) {
    this.router.navigate(['character-select'], { queryParams: { characterId: characterId } }).then();
  }
}
