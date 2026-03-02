import React, { createContext, useState } from 'react';
import { registerPsychiatrist, checkServerConnection, registerUser, loginUser, createNote, fetchMyNotes } from '../api/Auth';

export const AuthContext = createContext();
export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);

  const onRegisterPsychiatrist = async (name, surname, email, password, psychiatryistRegisterCode) => {
 
    const result = await registerPsychiatrist({
            name,
            surname,
            email,
            password,
            psychiatryistRegisterCode
        });

        if (result.success) {
            console.log("Psychiatra zarejestrowany");

        }
        return result;
    };

    const onRegisterUser = async (name, surname, email, password, psychiatryistCode) => {
 
    const result = await registerUser({
            name,
            surname,    
            email,
            password,
            psychiatryistCode
        });

        if (result.success) {
            console.log("Użytkownik zarejestrowany");

        }
        return result;
    };
    const testConnection = async () => {
    console.log("Próba połączenia...");
    const result = await checkServerConnection();
    alert(result.message);
  };

  const onLogin = async (email, password) => {
    const result = await loginUser(email, password);

    if (result.success) {
      setToken(result.data.token);
      setUser(result.data.user);
      console.log("Zalogowano pomyślnie:", result.data.user.email);
    }
    return result; 
  };
  const onAddNote = async (content) => {
    if (!token) {
      console.log("Błąd: Brak tokena w Context");
      return { success: false, message: "Brak autoryzacji" };
    }

    const result = await createNote(content, token); 

    if (result.success || result.id) {
       console.log("Notatka dodana do bazy");
    }
    return result;
  };
    const onFetchNotes = async () => {
    if (!token) return []; 
    
    const result = await fetchMyNotes(token);
    console.log("Notatki pobrane (ilość):", result.length);
    return result; 
};

const onAnalyzeNotes = async (selectedIds) => {
    if (!token) {
      alert("Błąd: Nie jesteś zalogowany");
      return { success: false };
    }

    console.log("Rozpoczynam analizę dla ID:", selectedIds);
    const result = await analyzeNotes(selectedIds, token);

    if (result.success) {
      console.log("Analiza zakończona pomyślnie");
    } else {
      console.log("Analiza nie powiodła się", result.message);
    }
    
    return result;
  };

    return (
        <AuthContext.Provider value={{ onRegisterPsychiatrist, testConnection, onRegisterUser, onLogin, onAddNote, onFetchNotes, onAnalyzeNotes }}>
            {children}
        </AuthContext.Provider>
    );
};

