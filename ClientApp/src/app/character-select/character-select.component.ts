import {Component, OnInit} from '@angular/core';
import {Character, ComboNote, Move} from "../../models";
import {MoveListService} from "../../services/move-list.service";
import {ComboNoteService} from "../../services/combo-note.service";
import {DomSanitizer} from "@angular/platform-browser";
import {MatSelectChange} from "@angular/material/select";
import {ActivatedRoute} from "@angular/router";
import {firstValueFrom, Observable} from "rxjs";
import {HEADER_TIMESTAMP_UPDATE} from "../header/header.component";
import {CharacterService} from "../../services/character.service";

@Component({
  selector: 'app-character-select',
  templateUrl: './character-select.component.html',
  styleUrls: ['./character-select.component.css']
})
export class CharacterSelectComponent implements OnInit {
  public selectedCharacterId: string = "";
  public selectedCharacterName?: string = "";
  public characterDropdownList: Character[] = [];
  public moveList: Move[] = [];
  public lastUpdated: string = "";
  public isLoading: boolean = false;
  public comboNotes: ComboNote[] = [];
  public displayVideoFlags: boolean[] = [];
  public videoSafeUrls: any[] = [];

  constructor(private moveListService: MoveListService,
              private comboNoteService: ComboNoteService,
              private characterService: CharacterService,
              private activatedRoute: ActivatedRoute) {
  }
  ngOnInit() {
    this.activatedRoute.data.subscribe((data: any) =>
        this.characterDropdownList = data.characters
    );

    this.activatedRoute.queryParams.subscribe((params: any) => {
      this.selectedCharacterId = params.characterId;

      this.selectedCharacterName = this.characterService.availableCharacters
        .find(c => c.characterId === this.selectedCharacterId)?.name;

      this.fetchCurrentMoveList();

      this.pullComboNoteData();
    });
  }

  private pullComboNoteData() {
    this.comboNoteService.getComboNotes(this.selectedCharacterId).subscribe({
      next: (notes: ComboNote[]) => {
        this.comboNotes = notes;

        this.displayVideoFlags = [];

        this.videoSafeUrls = [];
      },
      error: err => console.error(err)
    });
  }

  // private async pullMoveData(): Promise<void> {
  //   /**
  //    * check if scrapping is required
  //    * if the data is older than 3 days then automatically scrap move list
  //    */
  //   const timeStampReq: Observable<any> = this.moveListService.getLastUpdatedTimestamp(this.selectedCharacter);
  //   const timeStamp: string = await firstValueFrom(timeStampReq).catch(err => console.error(err));
  //
  //   const end = new Date();
  //   const start = new Date(timeStamp);
  //   const threeDaysInSeconds: number = 259200;
  //   const elapsedTimeInSeconds: number = (Math.floor(end.getTime()) - Math.floor(start.getTime())) / 1000;
  //
  //   if (elapsedTimeInSeconds > threeDaysInSeconds) {
  //     await this.scrapMoveList().catch(err => console.error(err));
  //
  //     this.fetchCurrentMoveList();
  //   }
  //   else {
  //     this.fetchCurrentMoveList();
  //   }
  // }

  private fetchCurrentMoveList(): void {
    this.moveListService.getMoveList(this.selectedCharacterId).subscribe({ next: (moves: Move[]) => {
      this.moveList = moves.sort(this.sortMoves);

      this.updateMoveListTimestamp();
    },
      error: err => console.error(err)
    });
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

  public updateMoveListTimestamp(): void {
    this.moveListService.getLastUpdatedTimestamp(this.selectedCharacterId).subscribe((timestamp: string) => {
      if (!timestamp) {
        this.lastUpdated = "";

        this.moveListService.lastUpdatedTimestamp = undefined;

        return;
      }

      this.moveListService.lastUpdatedTimestamp = new Date(timestamp);

      this.lastUpdated = this.moveListService.lastUpdatedTimestamp.toLocaleString();

      HEADER_TIMESTAMP_UPDATE.emit(this.lastUpdated);
    });
  }

  public addComboNote() {
    this.comboNotes.push({
      characterId: this.selectedCharacterId,
      comboString: "",
      noteContext: "",
      footageUrl: "",
      longestStreak: 0
    });
  }

  public saveComboNote(index: number): void {
    this.isLoading = true;

    // if ID is not present then we know the note is just added on the UI side
    const request: Observable<any> = !this.comboNotes[index].comboNoteId
      ? this.comboNoteService.saveComboNote(this.comboNotes[index])
      : this.comboNoteService.updateComboNote(this.comboNotes[index]);

    request.subscribe({ next: (data) => {
        if (!this.comboNotes[index].comboNoteId) {
          // refresh the combo notes so new IDs will be populated
          this.pullComboNoteData();
        }
      },
      error: err => console.error(err)
    });
  }

  public deleteComboNote(index: number): void {
    // if ID is not present then we know the note is just added on the UI side
    if (!this.comboNotes[index].comboNoteId) {
      this.comboNotes.splice(index, 1);
    }
    else {
      this.isLoading = true;

      this.comboNoteService.deleteComboNote(this.comboNotes[index].comboNoteId!).subscribe({ next: (data: string) => {
        console.log(data);

        this.isLoading = false;

        this.pullComboNoteData();
      },
        error: err => console.error(err)
      });
    }
  }
}
