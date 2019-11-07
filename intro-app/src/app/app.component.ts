import { Component } from '@angular/core';

@Component({
  // the "selector" (think CSS selector)
  // tells angular what elements in HTML to replace with
  // this component
  selector: 'app-root',
  // here we connect the component TS class (logic) to
  // the HTML template (display/view)
  // (you can also use "inline template" with template property)
  templateUrl: './app.component.html',
  // template: `
  //   <div>
  //   </div>
  // `,

  // in angular, CSS is encapsulated with each component.
  // you are free to write that css as though that component
  // were the whole world
  // and we can have several in this array
  styleUrls: ['./app.component.css'],
  // inline styles are also an option
  // styles: [
  //   `div { color: black }`
  // ]

  // we also have "animations" to configure here
})
export class AppComponent {
  title = 'intro-app';
}
