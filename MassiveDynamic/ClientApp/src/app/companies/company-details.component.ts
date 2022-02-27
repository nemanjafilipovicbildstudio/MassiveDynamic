import { HttpClient, HttpEventType, HttpParams, HttpResponse } from "@angular/common/http";
import { Component, EventEmitter, Inject, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { GlobalService } from "src/services/global-service";

@Component({
    selector: 'app-company-details',
    templateUrl: './company-details.component.html',
    styleUrls: ['./company-details.component.css']
  })
  export class CompanyDetailsComponent implements OnInit {
    company: Company;
    id: string;
    editContactPersonId: string;
    addContactPersonFlag: boolean = false;
    contactPersonForm: FormGroup;
    uploadProgress: number;
    uploadMessage: string;
    uploadError: string;
    downloadProgress: number;
    downloadMessage: string;
    documents: Document[];
    userRoles: string[];

    @Output() public onUploadFinished = new EventEmitter();

    constructor(
      private http: HttpClient, 
      @Inject('BASE_URL') private baseUrl: string, 
      private globalService: GlobalService,
      private route: ActivatedRoute,
      private formBuilder: FormBuilder
    ) {}

    async ngOnInit(): Promise<void> {
      this.id = this.route.snapshot.params['id'];
      this.userRoles = JSON.parse(sessionStorage.getItem('userRoles')) as string[];
      this.createForm();
      this.getCompany();
      if(this.canAccessDocuments()){
        this.getDocuments();
      }
    }

    private getCompany() {
      this.http.get<Company>(this.baseUrl + 'company?id=' + this.id).subscribe(result => {
          this.company = result;
      }, error => {
          console.error(error);
          this.globalService.checkAccessDenied(error);
      });
    }

    createForm(){
      this.contactPersonForm = this.formBuilder.group({
        firstName: ["", Validators.required],
        lastName: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
        phoneNumber: ["", Validators.required],
        address: ["", Validators.required],
      });
    }

    get f() { return this.contactPersonForm.controls; }

    isAddButtonEnabled(){
      return this.userRoles.includes('Admin') || this.userRoles.includes('Secretary');
    }

    isEditContactPersonButtonEnabled(){
      return this.userRoles.includes('Admin') || this.userRoles.includes('Secretary');
    }

    isDeleteButtonEnabled(){
      return this.userRoles.includes('Admin');
    }

    canAccessDocuments() {
      return this.userRoles.includes('Admin') || this.userRoles.includes('Secretary');
    }

    addContactPerson(){
      this.addContactPersonFlag = true;
    }

    saveContactPerson(id: string){
      const contactPerson: ContactPerson = {
        id: null,
        companyId: this.company.id,
        firstName : this.contactPersonForm.controls['firstName'].value,
        lastName : this.contactPersonForm.controls['lastName'].value,
        email : this.contactPersonForm.controls['email'].value,
        phoneNumber : this.contactPersonForm.controls['phoneNumber'].value,
        address : this.contactPersonForm.controls['address'].value
      };

      this.http.post(this.baseUrl + 'contactperson', contactPerson)
        .subscribe(res => {
          this.addContactPersonFlag = false;
          this.createForm();
          this.getCompany();
      }, error => {
        console.error(error);
        this.globalService.checkAccessDenied(error);
      });
    }

    cancelAddingContactPerson(){
      this.addContactPersonFlag = false;
      this.createForm();
    }

    editContactPerson(contactPersonId: string){
      this.editContactPersonId = contactPersonId;
      this.cancelAddingContactPerson();
      const contactPerson = this.company.contactPersons.find(x => x.id == contactPersonId);
      this.contactPersonForm.controls["firstName"].setValue(contactPerson.firstName);
      this.contactPersonForm.controls["lastName"].setValue(contactPerson.lastName);
      this.contactPersonForm.controls["email"].setValue(contactPerson.email);
      this.contactPersonForm.controls["phoneNumber"].setValue(contactPerson.phoneNumber);
      this.contactPersonForm.controls["address"].setValue(contactPerson.address);
    }

    saveEditedContactPerson(){
      const contactPerson: ContactPerson = {
        id: this.editContactPersonId,
        companyId: this.company.id,
        firstName : this.contactPersonForm.controls['firstName'].value,
        lastName : this.contactPersonForm.controls['lastName'].value,
        email : this.contactPersonForm.controls['email'].value,
        phoneNumber : this.contactPersonForm.controls['phoneNumber'].value,
        address : this.contactPersonForm.controls['address'].value
      };

      this.http.put(this.baseUrl + 'contactperson', contactPerson)
        .subscribe(res => {
          this.cancelEditContactPerson();
          this.getCompany();
      }, error => {
        console.error(error);
        this.globalService.checkAccessDenied(error);
      });
    }

    cancelEditContactPerson(){
      this.editContactPersonId = null;
      this.createForm();
    }

    isDeleteContactPersonEnabled(){

    }

    deleteContactPerson(id: string){
      if(confirm("Are you sure you want to delete this contact person?")){
        this.http.delete(this.baseUrl + 'contactperson?id=' + id)
          .subscribe(res => {
            this.getCompany();
        }, error => {
          console.error(error);
          this.globalService.checkAccessDenied(error);
        });
      }
    }

    public uploadFile = (files) => {
      if (files.length === 0) {
        return;
      }
      
      let fileToUpload = <File>files[0];
      const formData = new FormData();
      formData.append('file', fileToUpload, fileToUpload.name);
      
      this.http.post('https://localhost:5001/document/upload', formData, {reportProgress: true, observe: 'events', params: new HttpParams().set("companyId", this.id)})
        .subscribe(event => {
          if (event.type === HttpEventType.UploadProgress)
            this.uploadProgress = Math.round(100 * event.loaded / event.total);
          else if (event.type === HttpEventType.Response) {
            this.uploadMessage = 'Upload success.';
            this.uploadError = "";
            this.onUploadFinished.emit(event.body);
            this.getDocuments();
          }
        },
          error => {
            this.uploadError = error.error;
            this.uploadMessage = "";
          });
    }

    public downloadFile(documentId: string, documentName: string){
      const fileUrl = 'https://localhost:5001/document/download?id=' + documentId;
      this.http.get(fileUrl, { reportProgress: true, observe: 'events',  responseType: 'blob' })
        .subscribe((event) => {
          if (event.type === HttpEventType.UploadProgress)
            this.downloadProgress = Math.round((100 * event.loaded) / event.total);
          else if (event.type === HttpEventType.Response) {
            this.downloadMessage = 'Download success.';
            this.download(event, documentName);
          }
        }, error => {
          console.error(error);
          this.globalService.checkAccessDenied(error);
        });
    }

    private download(data: HttpResponse<Blob>, documentName: string) {
      const downloadedFile = new Blob([data.body], { type: data.body.type });
      const a = document.createElement('a');
      a.setAttribute('style', 'display:none;');
      document.body.appendChild(a);
      a.download = documentName;
      a.href = URL.createObjectURL(downloadedFile);
      a.target = '_blank';
      a.click();
      document.body.removeChild(a);
    }

    getDocuments() {
      const fileUrl = 'https://localhost:5001/document/getCompanyDocuments?companyId=' + this.id;
      this.http.get<Document[]>(fileUrl)
        .subscribe(res => {
          this.documents = res;
        }, error => {
          console.error(error);
          this.globalService.checkAccessDenied(error);
        })
    }

    deleteDocument(id: string){
      if(confirm("Are you sure you want to delete this document?")){
        const fileUrl = 'https://localhost:5001/document/?id=' + id;
        this.http.delete(fileUrl)
          .subscribe(res => {
            this.getDocuments();
          }, error => {
            console.error(error);
            this.globalService.checkAccessDenied(error);
          })
      }
    }
}

interface Company {
  id: string;
  name: string;
  address: number;
  contactPersons: ContactPerson[]
}

interface ContactPerson {
  id: string;
  companyId: string,
  firstName: string;
  lastName: number;
  email: string;
  phoneNumber: string;
  address: string;
}

interface Document {
  id: string,
  name: string,
  uploadTime: string
}