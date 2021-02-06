import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { url } from 'inspector';
import { BaseFile } from '../table/fileinfo';
import { ButtonStyle, ColumnBtnEvent, TableColumnButton } from '../table/tablecolumn';

@Component({
  selector: 'app-tablebutton',
  templateUrl: './tablebutton.component.html',
  styleUrls: ['./tablebutton.component.sass']
})
export class TablebuttonComponent<T extends BaseFile> implements OnInit {

  @Input() btn: TableColumnButton<T>;
  @Input() item: T;
  btnStyle = ButtonStyle;
  constructor(public http: HttpClient) { }

  ngOnInit(): void {
    console.log(this.btn.isHidden ? this.btn.isHidden(this.item) : false, this.item);
  }

  isHidden(): boolean {
    return this.btn.isHidden ? this.btn.isHidden(this.item) : true;
  }

  getUrl(): string {
    return this.btn.getUrl ? this.btn.getUrl(this.item) : this.btn.url;
  }


  click() {
    if (this.btn.type) {
      switch (this.btn.type) {
        case ColumnBtnEvent.del:
          this.http.delete(`${this.getUrl()}/${this.item.id}`).subscribe(res => {

          });
      }
    }
  }

}
