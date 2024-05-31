import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {Character, ComboNote, Move} from "../../models";
import {MoveListService} from "../../services/move-list.service";
import {MatSelectChange} from "@angular/material/select";
import {DomSanitizer} from "@angular/platform-browser";
import {ComboNoteService} from "../../services/combo-note.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  public currentInputRef: any = "";
  public selectedCharacter: string = "";
  public characterDropdownList: Character[] = [];
  public moveList: Move[] = [];
  public lastUpdated: string = "";
  public isLoading: boolean = false;
  public comboNotes: ComboNote[] = [];
  public displayVideoFlags: boolean[] = [];
  public videoSafeUrls: any[] = [];

  constructor(private characterService: CharacterService,
              private moveListService: MoveListService,
              private comboNoteService: ComboNoteService,
              private sanitizer: DomSanitizer) {
  }
  ngOnInit() {
    this.characterService.getCharacters().subscribe((characters: Character[]) => {
      this.characterService.availableCharacters = characters;

      this.characterDropdownList = characters;
    })
  }

  public scrapMoveList() {
    this.isLoading = true;

    this.moveListService.putScrapMoveList(this.selectedCharacter).subscribe((inputList: string[]) => {
      console.log(inputList);

      this.isLoading = false;

      this.pullMoveData();
    });
  }

  public onSelectCharacter($event: MatSelectChange) {
    this.selectedCharacter = $event.value;

    this.pullMoveData();

    this.pullComboNoteData();
  }

  private pullComboNoteData() {
    this.comboNoteService.getComboNotes(this.selectedCharacter).subscribe((notes: ComboNote[]) => {
      this.comboNotes = notes;

      this.displayVideoFlags = [];

      this.videoSafeUrls = [];
    });
  }

  private pullMoveData(): void {
    this.moveListService.getMoveList(this.selectedCharacter).subscribe((moves: Move[]) => {
      this.moveList = moves.sort(this.sortMoves);
    });

    this.updateMoveListTimestamp();
  }

  private sortMoves(a: Move, b: Move): number {
    if (a.input.length > b.input.length) {
      return 1;
    }
    else if (a.input.length === b.input.length) {
      return 0;
    }
    else{
      return -1;
    }
  }

  public updateMoveListTimestamp() {
    this.moveListService.getLastUpdatedTimestamp(this.selectedCharacter).subscribe((timestamp: string) => {
      if (!timestamp) {
        this.lastUpdated = "";

        this.moveListService.lastUpdatedTimestamp = undefined;

        return;
      }

      this.moveListService.lastUpdatedTimestamp = new Date(timestamp);

      this.lastUpdated = this.moveListService.lastUpdatedTimestamp.toLocaleString();
    });
  }

  public addComboNote() {
    this.comboNotes.push({
      characterId: this.selectedCharacter,
      comboString: "",
      noteContext: "",
      footageUrl: "",
      longestStreak: 0
    });
  }

  public saveComboNote(index: number): void {
    this.isLoading = true;

    // if ID is not present then we know the note is just added on the UI side
    if (!this.comboNotes[index].comboNoteId) {
      this.comboNoteService.saveComboNote(this.comboNotes[index]).subscribe((data: string) => {
        console.log(data);

        this.isLoading = false;

        // refresh the combo notes so new IDs will be populated
        this.pullComboNoteData();
      });
    }
    else {
      this.comboNoteService.updateComboNote(this.comboNotes[index]).subscribe((data: string) => {
        console.log(data);

        this.isLoading = false;
      });
    }
  }

  public deleteComboNote(index: number): void {
    // if ID is not present then we know the note is just added on the UI side
    if (!this.comboNotes[index].comboNoteId) {
      this.comboNotes.splice(index, 1);
    }
    else {
      this.isLoading = true;

      this.comboNoteService.deleteComboNote(this.comboNotes[index].comboNoteId!).subscribe((data: string) => {
        console.log(data);

        this.isLoading = false;

        this.pullComboNoteData();
      });
    }
  }

  public toggleShowVideo(index: number) {
    // refactor this later
    this.videoSafeUrls[index] = this.sanitizer.bypassSecurityTrustResourceUrl(this.comboNotes[index].footageUrl);

    this.displayVideoFlags[index] = !this.displayVideoFlags[index];
  }

  public onFocus($event: FocusEvent) {
    // console.log($event);
    // this.currentInputRef = $event.currentTarget
  }
}
