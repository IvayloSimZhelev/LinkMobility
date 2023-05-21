import {
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import {
  SelectItem,
} from 'primeng/api';
import { Customer } from '../models/cutomer';

@Component({
  selector: 'app-dialogs',
  templateUrl: './dialogs.component.html',
  styleUrls: ['./dialogs.component.scss'],
})
export class DialogsComponent {
  @Input() customerDialog: boolean;
  @Input() customerValue: any;
  @Input() fields: any[];
  @Input() cols: any[];

  @Output() messageEvent1 = new EventEmitter<boolean>();
  @Output() messageEvent2 = new EventEmitter<boolean>();
  @Output() messageEvent3 = new EventEmitter<any>();

  customers: Customer[] = [];
  customer: any;
  loading: boolean = true;
  matchModeOptions: SelectItem[];
  submitted: boolean;
  @Input() subscriptionState: { label: string; value: any }[];
  myDefaultDate: Date;
  headers: any = 'Car Details';

  constructor() {}

  ngOnInit() {
    this.loading = true;
  }

  hideDialog() {
    this.customerDialog = false;
    this.messageEvent1.emit(this.customerDialog);
  }

  saveCustomer() {
    this.customerDialog = false;
    this.submitted = true;

    this.messageEvent1.emit(this.customerDialog);
    this.messageEvent2.emit(this.submitted);
    this.messageEvent3.emit(this.customerValue);
  }

  createId(): number {
    let id = this.customers[this.customers.length - 1].id;
    id += id;
    return id;
  }
}
