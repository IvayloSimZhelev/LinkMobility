import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccordionModule } from 'primeng/accordion';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DatagridComponent } from './datagrid/datagrid.component';
import { RouterModule, Routes } from '@angular/router';
import { TableModule } from 'primeng/table';
import { HttpClientModule } from '@angular/common/http';
import { MenubarModule } from 'primeng/menubar';
import { TreeTableModule } from 'primeng/treetable';
import { NodeService } from './services/node.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { SliderModule } from 'primeng/slider';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { ContextMenuModule } from 'primeng/contextmenu';
import { ToastModule } from 'primeng/toast';
import { ProgressBarModule } from 'primeng/progressbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FileUploadModule } from 'primeng/fileupload';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MessageService, ConfirmationService } from 'primeng/api';
import { DialogsComponent } from './dialogs/dialogs.component';
import { CheckboxModule } from 'primeng/checkbox';
import { AuthService } from './services/auth.service';
import { CustomerService } from './services/customer.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    DatagridComponent,
    DialogsComponent    
  ],
  imports: [
    
    CheckboxModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    AccordionModule,
    TableModule,
    HttpClientModule,
    MenubarModule,
    TreeTableModule,
    BrowserAnimationsModule,
    CalendarModule,
    SliderModule,
    DialogModule,
    ContextMenuModule,
    DropdownModule,
    InputTextModule,
    ProgressBarModule,
    FormsModule,
    BrowserModule,
    MultiSelectModule,
    ButtonModule,
    ToastModule,
    HttpClientModule,
    FileUploadModule,
    ToolbarModule,
    RatingModule,
    RadioButtonModule,
    InputNumberModule,
    ConfirmDialogModule,
    InputTextareaModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
      { path: 'datagrid', component: DatagridComponent, pathMatch: 'full' },
      { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    ]),
  ],
  providers: [NodeService, AuthService, CustomerService,  MessageService, ConfirmationService],
  bootstrap: [AppComponent],
})
export class AppModule {}
