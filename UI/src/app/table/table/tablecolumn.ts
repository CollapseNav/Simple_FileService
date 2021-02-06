export interface TableConfig<T> {
  url?: string;
  downloadUrl?: string;
  columns: TableColumn<T>[];
}
export interface TableColumn<T> {
  label: string;
  valIndex?: string;
  format?: (item: T) => any;
  default?: any;
  sort?: boolean;
  select?: boolean;
  buttons?: TableColumnButton<T>[];
}

export interface TableColumnButton<T> {
  content: string;
  style?: ButtonStyle;
  type?: ColumnBtnEvent;
  icon?: string;
  hidden?: boolean;
  isHidden?: (item: T) => any;
  disabled?: boolean;
  isDisabled?: (item: T) => any;
  url?: string;
  getUrl?: (item: T) => any;
  color?: string;
  click?: (item: T) => any;
  toolTip?: {
    content: string;
    showDelay?: number;
  };
}

export enum ColumnBtnEvent {
  add, del, download, edit
}


export enum ButtonStyle {
  link, raised, stroked, flat, icon, fab, mini
}
