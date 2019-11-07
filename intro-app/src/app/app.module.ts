import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FridgeComponent } from './fridge/fridge.component';
import { HttpClientModule } from '@angular/common/http';

// this is a decorator
// decorators are preview TS syntax
// Angular defines several important decorators
// whose job is to tell angular to turn
// a given TS class into a special angular thing:
//   module - NgModule
//   component - Component
//   services - Injectable
//   directive - Directive
@NgModule({
  // every component must be declared in exactly one module
  declarations: [
    AppComponent,
    FridgeComponent
  ],
  // if anything in this module needs anything from another module,
  // we have to reference that second module here.
  // we know about "TS import" that connects TS modules
  // this is "NG import" that connect NG modules
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  // we can register services for DI here (scoped to this module)
  providers: [],
  // only the root module needs this entry
  // and it should contain the root component
  bootstrap: [AppComponent]
})
export class AppModule { }
