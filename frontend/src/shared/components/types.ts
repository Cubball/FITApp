import { ButtonHTMLAttributes, InputHTMLAttributes } from "react";

export interface IInputAttributes extends InputHTMLAttributes<HTMLInputElement> {
  label: string;
}

export interface IButtonAttributes extends ButtonHTMLAttributes<HTMLButtonElement> {
  text: string;
}
