import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post.component';
import { PostServiceApi } from '../apis'; import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MdButtonModule, MdIconModule, MdInputModule } from '@angular/material';
import { SocialInteractionsModule } from '../social-interactions/social-interactions.module';
@NgModule({
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    MdButtonModule,
    MdIconModule,
    MdInputModule,
    SocialInteractionsModule
  ],
  declarations: [PostComponent],
  exports: [PostComponent],
  providers: [PostServiceApi]
})
export class PostModule { }
