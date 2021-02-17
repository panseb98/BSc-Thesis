import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './authorization/login/login.component';
import { RegistrationComponent } from './authorization/registration/registration.component';
import { PersonalDataComponent } from './authorization/registration/personal-data/personal-data.component';
import { ExpericenceComponent } from './authorization/registration/expericence/expericence.component';
import { EducationComponent } from './authorization/registration/education/education.component';
import { SkillsComponent } from './authorization/registration/skills/skills.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { NotifierModule, NotifierOptions } from "angular-notifier";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http'; 
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule, MatRippleModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { JobsListComponent } from './jobs/jobs-list/jobs-list.component';
import { SubmitComponent } from './authorization/registration/submit/submit.component';
import { UserDataComponent } from './authorization/user/user-data/user-data.component';
import { DatePipes } from './helpers/date.pipe';
import { DatePipe } from '@angular/common';
import { SingleComponentComponent } from './jobs/single-component/single-component.component';
import { FirstRegistrationComponent } from './authorization/first-registration/first-registration.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegistrationModalComponent } from './authorization/registration-modal/registration-modal.component';
import {FlexLayoutModule} from '@angular/flex-layout';
import { UserJobTitlesComponent } from './authorization/user/user-job-titles/user-job-titles.component';
import { JobsTitleListComponent } from './jobs/jobs-title-list/jobs-title-list.component';
import { JobsSkillsListComponent } from './jobs/jobs-skills-list/jobs-skills-list.component';
import { JobSkillAddComponent } from './jobs/job-skill-add/job-skill-add.component';
import { JobTitleAddComponent } from './jobs/job-title-add/job-title-add.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminUserListComponent } from './admin/admin-user-list/admin-user-list.component';
import { AdminEditJobSynchroComponent } from './admin/admin-edit-job-synchro/admin-edit-job-synchro.component';
import { AdminRequestListComponent } from './admin/admin-request-list/admin-request-list.component';

const customNotifierOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: "right",
      distance: 12
    },
    vertical: {
      position: "top",
      distance: 12,
      gap: 10
    }
  },
  theme: "material",
  behaviour: {
    autoHide: 5000,
    onClick: "hide",
    onMouseover: "pauseAutoHide",
    showDismissButton: true,
    stacking: 4
  },
  animations: {
    enabled: true,
    show: {
      preset: "slide",
      speed: 300,
      easing: "ease"
    },
    hide: {
      preset: "fade",
      speed: 300,
      easing: "ease",
      offset: 50
    },
    shift: {
      speed: 300,
      easing: "ease"
    },
    overlap: 150
  }
};
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    PersonalDataComponent,
    ExpericenceComponent,
    EducationComponent,
    SkillsComponent,
    NavBarComponent,
    JobsListComponent,
    SubmitComponent,
    DatePipes,
    SingleComponentComponent,
    UserDataComponent,
    FirstRegistrationComponent,
    RegistrationModalComponent,
    UserJobTitlesComponent,
    JobsTitleListComponent,
    JobsSkillsListComponent,
    JobSkillAddComponent,
    JobTitleAddComponent,
    AdminDashboardComponent,
    AdminUserListComponent,
    AdminEditJobSynchroComponent,
    AdminRequestListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NotifierModule.withConfig(customNotifierOptions),
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    HttpClientModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    NgbModule,
  ],
  exports: [
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
