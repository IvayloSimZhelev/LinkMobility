import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';

import {
  ConfirmationService,
  MessageService,
  FilterMatchMode,
  SelectItem,
  SortEvent,
  LazyLoadEvent,
} from 'primeng/api';
import { Table } from 'primeng/table';
import { FilterService } from 'primeng/api';
import { AuthService } from '../services/auth.service';
import { Customer } from '../models/cutomer';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-datagrid',
  templateUrl: './datagrid.component.html',
  styles: [
    `
      :host ::ng-deep .p-dialog .product-image {
        width: 150px;
        margin: 0 auto 2rem auto;
        display: block;
      }
    `,
  ],
  providers: [MessageService, ConfirmationService, DatePipe],
  styleUrls: ['./datagrid.component.scss'],
})
export class DatagridComponent implements OnInit {
  dt: Table;
  rowsPerPage: number = 10; // Default value for rows per page
  get filteredCustomers(): Customer[] {
    return this.customers;    
  }

  isLoggedIn :boolean;

  selectedCustomer : Customer;
  filteredSecondGrid: Customer[];
  totalRecordsValues : number = 0;
  customers : Customer[] = [];
  customer : Customer ={
    subscriptionState: '',
    invoices: [] = [],
    numberOfInvoices: 0
  }
  selectedRows: any[] = [];
  cols: any[];
  first: number = 1;
  last: number = 0;
  loading: boolean = true;
  matchModeOptions: SelectItem[];
  submitted: boolean;
  customerDialog: boolean;
  subscriptionState: { label: string; value: any }[];
  customerForm: any;
  searchTerm: string;

  receiveCustomerDialog($event) {
    this.customerDialog = $event;
  }

  receiveSubmitted($event) {
    this.submitted = $event;
  }

  receiveCustomerValue($event) {
    this.customer = $event;
    this.saveCustomer();
  }

  constructor(
    private customerService: CustomerService,
    private filterService: FilterService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private datePipe: DatePipe,
    private authService: AuthService,
  ) {
    this.isLoggedIn = authService.getIsLoggedIn();

    this.authService.loginStatusChanged.subscribe((status: boolean) => {
     this.isLoggedIn = status;
     this.authService.setIsLoggedIn(status);
   });
  }

  login(): void {
    this.authService.login();
  }


  ngOnInit() {
    
    const customFilterName = 'custom-equals';
    this.loading = false;     

    this.cols = [
      {
        field: 'companyName',
        header: 'Company Name',
        name: 'companyName',
        type: 'text',

        label: 'Company Name',
        required: true,
      },
      {
        field: 'address',
        header: 'Address',
        name: 'address',
        type: 'text',
        label: 'Address ',
        required: true,
      },
      {
        field: 'state',
        header: 'State',
        name: 'state',
        type: 'text',
        label: 'State',
        required: true,
      },
      {
        field: 'country',
        header: 'Country',
        name: 'country',
        type: 'text',
        label: 'Country',
        required: true,
      },
    ];

    this.matchModeOptions = [
      { label: 'Custom Equals', value: customFilterName },
      { label: 'Starts With', value: FilterMatchMode.STARTS_WITH },
      { label: 'Contains', value: FilterMatchMode.CONTAINS },
    ];

    this.subscriptionState = [
      { label: 'New', value: 'New' },
      { label: 'Active', value: 'Active' },
      { label: 'Suspended', value: 'Suspended' },
    ];

    this.filterService.register(customFilterName, (value, filter): boolean => {
      if (filter === undefined || filter === null || filter.trim() === '') {
        return true;
      }

      if (value === undefined || value === null) {
        return false;
      }

      return value.toString() === filter.toString();
    });

    this.getCustomers(1);
  }
  
  onPageChange(event: any) {    
    const page = event.first / event.rows + 1;
    setTimeout(() => {
      const page = event.first / event.rows + 1;
      this.getCustomers(page);
      }, 500);
  }

  SearchByCompanyName() {
    const companyName = this.searchTerm;
    const page = 1; 
    const pageSize = this.rowsPerPage;
  
    this.customerService.getCustomers(companyName, page, pageSize).subscribe(response => {
      this.customers = response.data;
      this.totalRecordsValues = response.total;
    });
  }

  getCustomers(page: number) {
    let companyName: string = '';
    let pageSize: number = this.rowsPerPage;
  
    this.customerService.getCustomers(companyName, page, pageSize).subscribe({
      next: (customers) => {
        this.customers = customers.data;
        this.totalRecordsValues = customers.total;
        this.last = customers.total;
      },
      error: (error) => {
        console.error('Error getting customers:', error);
      },
      complete: () => {
        console.info('Complete');
      }
    });
  }

  onRowSelect(selectedData: any) {
     this.filteredSecondGrid = this.filteredCustomers.filter(item => item.id === selectedData.data.id);
     this.selectedCustomer = selectedData.data;
  }

  clear(dt: Table) {
    dt.clear();
    this.getCustomers(1);
  }

  openNew() {
    this.customer = {
      companyName:'',
      address: '',
      state: '',
      country: [],
      subscriptionState: '',
      invoices: [],
      numberOfInvoices: 0
    };
    this.submitted = false;
    this.customerDialog = true;
  }

  editCustomer(customer: Customer) {
    debugger;
    this.customer = { ...customer };
    this.customerDialog = true;
  }

  deleteCustomer(customer: Customer) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + customer.companyName + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.customers = this.customers.filter((val) => val.id !== customer.id);
        this.customerService.deleteCustomer(customer.id).subscribe({
          next: () => (this.customer = {
            subscriptionState: '',
            invoices: [],
            numberOfInvoices: 0
          }),
          error: (e) => console.error(e),
          complete: () => console.info('complete'),
        });
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Customer Deleted',
          life: 3000,
        });
      },
    });
  }

  hideDialog() {
    this.customerDialog = false;
    this.submitted = false;
  }

  saveCustomer() {    
      this.submitted = true;
      if (this.customer.address.trim()) {
        if (this.customer.id) {
          this.customerService.updateCustomer(this.customer.id, this.customer).subscribe({
            next: (customer) => (this.customer = customer),
            error: (e) => console.error(e),
            complete: () => console.info('complete'),
          });
          this.customers[this.findIndexById(this.customer.id)] = this.customer;
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Customer Updated',
            life: 3000,
          });
        } else {
          this.customer.id = this.createId();
          this.customerService.createCustomer(this.customer).subscribe({
            next: (customer) => (this.customer = customer),
            error: (e) => console.error(e),
            complete: () => console.info('complete'),
          });
          this.customers.push(this.customer);
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Customer Created',
            life: 3000,
          });
        }

        this.customers = [...this.customers];
      }
  }

  findIndexById(id: number): number {
    let index = -1;
    for (let i = 0; i < this.customers.length; i++) {
      if (this.customers[i].id === id) {
        index = i;
        break;
      }
    }
    return index;
  }

  createId(): number {
    let id = this.customers[this.customers.length - 1].id;
    id += id;
    return id;
  }

  formatDateFromDate(date) {
    let month = date.getMonth() + 1;
    let day = date.getDate();

    if (month < 10) {
      month = '0' + month;
    }

    if (day < 10) {
      day = '0' + day;
    }

    return date.getFullYear() + '-' + month + '-' + day;
  }
}
