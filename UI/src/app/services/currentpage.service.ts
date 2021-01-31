import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TableApi } from '../api/tableApi';
import { Dir, MFile, SizeType } from '../table/table/fileinfo';

@Injectable({
  providedIn: 'root'
})
export class CurrentpageService {

  private tablePage: Dir[] = [];

  constructor() { }

  getPageRoute(): Dir[] {
    return this.tablePage;
  }

  getCurrentPage(): Dir {
    return this.tablePage[this.tablePage.length - 1];
  }

  getCurPageSub(): Observable<Dir> {
    return of(this.tablePage[this.tablePage.length - 1]);
  }
  getRootPage(): Dir {
    return this.tablePage[0];
  }

  add(page: Dir): Dir[] {
    this.tablePage.push(page);
    return this.tablePage;
  }

  addNewDirCur(dir: Dir) {
    this.getCurrentPage().dirs.push(dir);
  }

  addNewFileCur(file: MFile) {
    this.getCurrentPage().files.push(file);
  }

  resize(size: number) {
    if (this.getSize(size, SizeType.B)) {
      return this.getSize(size, SizeType.B) + 'B';
    }
    if (this.getSize(size, SizeType.K)) {
      return this.getSize(size, SizeType.K) + 'K';
    }
    if (this.getSize(size, SizeType.M)) {
      return this.getSize(size, SizeType.M) + 'M';
    }
    if (this.getSize(size, SizeType.G)) {
      return this.getSize(size, SizeType.G) + 'G';
    }
    return 0;
  }
  private getSize(size: number, type: SizeType): string | boolean {
    var result = size / type;
    return result > 1 && result < 1024 ? result.toFixed(2) : false;
  }
}
