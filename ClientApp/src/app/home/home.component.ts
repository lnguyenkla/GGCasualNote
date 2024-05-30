import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {Character, Move} from "../../models";
import {MoveListService} from "../../services/move-list.service";
import {MatSelectChange} from "@angular/material/select";

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.css'],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public currentInputRef: any = "";
  public selectedCharacter: string = "";
  public characterDropdownList: string[] = [];
  public moveList: Move[] = [];

  constructor(private characterService: CharacterService,
              private moveListService: MoveListService) {
  }
  ngOnInit() {
    this.characterService.getCharacters().subscribe((characters: Character[]) => {
      this.characterService.availableCharacters = characters;

      this.characterDropdownList = characters.map(c => c.characterId);
    })
  }

  public onSelectCharacter($event: MatSelectChange) {
    this.selectedCharacter = $event.value;

    this.moveListService.getMoveList(this.selectedCharacter).subscribe((moves: Move[]) => {
      this.moveList = moves.sort((a: Move, b: Move) => {
        if (a.input.length > b.input.length) {
          return 1;
        }
        else if (a.input.length === b.input.length) {
          return 0;
        }
        else{
          return -1;
        }
      });
    });
  }

  public onFocus($event: FocusEvent) {
    // console.log($event);
    // this.currentInputRef = $event.currentTarget
  }
}
