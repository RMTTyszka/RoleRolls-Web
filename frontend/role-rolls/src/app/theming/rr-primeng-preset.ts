import { definePreset } from '@primeng/themes';
import Aura from '@primeng/themes/aura';

const RrPrimengPreset = definePreset(Aura, {
  semantic: {
    colorScheme: {
      light: {
        root: {
          surface: '#30596c'
        },
        primary: {
          color: '#30596c',
          0: '{primary.color}',
          100: '{primary.color}',
          200: '#30596c',
          300: '#30596c',
          400: '#30596c',
          500: '{primary.color}',
          600: '#30596c',
          700: '#30596c',
          800: '#30596c',
          900: '#30596c',
          950: '#30596c',
        },
        secondary: {
          0: '#f4f4f4',
          100: '#f4f4f4',
          200: '#f4f4f4',
          300: '#f4f4f4',
          400: '#f4f4f4',
          500: '#f4f4f4',
          600: '#f4f4f4',
          700: '#f4f4f4',
          800: '#f4f4f4',
          900: '#f4f4f4',
          950: '#f4f4f4',
        },
        info: {
          color: '#30596c',
          background: '#30596c',
          0: '#f4f4f4',
          100: '#f4f4f4',
          200: '#f4f4f4',
          300: '#f4f4f4',
          400: '#f4f4f4',
          500: '#f4f4f4',
          600: '#f4f4f4',
          700: '#f4f4f4',
          800: '#f4f4f4',
          900: '#f4f4f4',
          950: '#f4f4f4',
        },
        surface: {
          0: '#a3c4bc',
        }
      }
    }
  },

})
export default RrPrimengPreset;
