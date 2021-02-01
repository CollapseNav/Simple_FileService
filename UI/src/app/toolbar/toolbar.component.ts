import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CurrentpageService } from '../services/currentpage.service';
import { TableComponent } from '../table/table.component';
import { NewdirComponent } from './newdir/newdir.component';
import { UploadService } from '../services/upload.service';
import { UploadComponent } from './upload/upload.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.sass']
})
export class ToolbarComponent implements OnInit {
  @Input() table: TableComponent;

  @Output() collapse = new EventEmitter<boolean>();

  isHandset: boolean = false;

  constructor(public cur: CurrentpageService,
    public dialog: MatDialog,
    public uploadServ: UploadService,
    public http: HttpClient) {
  }

  ngOnInit(): void {
  }

  sidernavToggle() {
    this.collapse.emit();
  }

  addNewFiles() {
    const newdia = this.dialog.open(UploadComponent, { minWidth: 350 });
    newdia.afterClosed().subscribe(() => {
      this.table.justSetableData(this.cur.getCurrentPage());
    });
  }

  addNewFolder() {
    const newdia = this.dialog.open(NewdirComponent, { minWidth: 200 });
    newdia.afterClosed().subscribe(() => {
      this.table.justSetableData(this.cur.getCurrentPage());
    });
  }

}
