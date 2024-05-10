import { toast } from "react-toastify";

export const createOnError = (text: string) => () => {
  toast(text, {
    type: 'error'
  });
};
