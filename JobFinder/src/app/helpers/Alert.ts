import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

export class Alert{

    
    private static  _instance : Alert;
    
    private constructor(private _snackBar: MatSnackBar) {}

    static getInstance(snackBar: MatSnackBar) : Alert{
        if(!Alert._instance){
            Alert._instance = new Alert(snackBar)
        }
        return this._instance;
    }

    openWrongAlert(message : string){
        let config = new MatSnackBarConfig();
    
        config.panelClass = 'wrong-message';
        config.verticalPosition = 'top';
        config.duration= 2500;
        
        this._snackBar.open(message, '',config);
        
        
    }
    openSuccessAlert(message : string){
        let config = new MatSnackBarConfig();

        config.panelClass = 'success-message';
        config.verticalPosition = 'top';
        config.duration= 2500;
        
        this._snackBar.open(message, '',config);
    }

}