import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {MoveListService} from "../../services/move-list.service";
import {MatSelectChange} from "@angular/material/select";
import {DomSanitizer} from "@angular/platform-browser";
import {ComboNoteService} from "../../services/combo-note.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {

  public onFocus($event: FocusEvent) {
    // console.log($event);
    // this.currentInputRef = $event.currentTarget
  }
}
