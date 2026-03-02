const API_URL = 'http://192.168.1.13:5046';


export const checkServerConnection = async () => {
    console.log("Wysyłam fetch do:", `${API_URL}/api/Auth/check-db`);
    
    const controller = new AbortController();
    const id = setTimeout(() => controller.abort(), 5000);

    try {
        const response = await fetch(`${API_URL}/api/Auth/check-db`, {
            method: 'GET',
            signal: controller.signal,
        });
        clearTimeout(id);

        console.log("Otrzymano odpowiedź status:", response.status);
        const data = await response.json();
        return { success: true, message: "Połączenie OK: " + JSON.stringify(data) };
    } catch (error) {
        clearTimeout(id);
        console.log("Błąd w fetch:", error.message);
        
        if (error.name === 'AbortError') {
            return { success: false, message: "Błąd: Przekroczono czas oczekiwania" };
        }
        return { success: false, message: "Błąd sieci: " + error.message };
    }
};
export const loginUser = async (email, password) => {
  try {
    const response = await fetch(`${API_URL}/api/Auth/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  });

  const json = await response.json();

    if (response.ok) {
      return { success: true, data: json }; 
    } else {
      return { success: false, message: json.message };
    }
  } catch (error) {
    return { success: false, message: "Błąd sieci" };
  }
};

export const registerPsychiatrist = async (psychData) => {
    try {
        const response = await fetch(`${API_URL}/api/Auth/register-psych`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(psychData),
        });
        return await response.json();
    } catch (error) {
        return { success: false, message: "Błąd połączenia z serwerem" };
    }
};

export const registerUser = async (userData) => {
    try {
        const response = await fetch(`${API_URL}/api/Auth/register-user`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userData),
        });
        return await response.json();
    } catch (error) {
        return { success: false, message: "Błąd połączenia z serwerem" };
    }
};

export const createNote = async (content, token) => {
  console.log("Wysyłam notatkę, token obecny:", !!token);
  try {
    const response = await fetch(`${API_URL}/api/Notes/add-note`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      },
      body: JSON.stringify({ content })
    });

    console.log("Status odpowiedzi:", response.status);

    if (!response.ok) {
      const errorText = await response.text();
      console.error("Serwer zwrócił błąd:", errorText);
      return { success: false, message: errorText };
    }

    return await response.json();
  } catch (error) {
    console.error("Błąd krytyczny fetch:", error.message);
    return { success: false, message: error.message };
  }
};
export const fetchMyNotes = async (token) => {
  try {
    const response = await fetch(`${API_URL}/api/notes/my-notes`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`, 
        'Content-Type': 'application/json',
      },
    });

    console.log("Status odpowiedzi HTTP:", response.status);

    const text = await response.text();
    console.log("Surowa odpowiedź (body):", text);

    if (!text || text.length === 0) {
      console.log("Serwer zwrócił pusty tekst!");
      return [];
    }

    const data = JSON.parse(text);
    return data.success ? data.notes : [];

  } catch (error) {
    console.error("Błąd pobierania notatek:", error);
    return [];
  }
};
export const analyzeNotes = async (ids, token) => {
  try {
    const response = await fetch(`${API_URL}/api/notes/analyze`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
      body: JSON.stringify({ ids }),
    });

    const text = await response.text();
    if (!response.ok) {
      console.error("Błąd serwera przy analizie:", text);
      return { success: false, message: "Błąd analizy AI" };
    }

    return JSON.parse(text);
  } catch (error) {
    console.error("Błąd sieci przy analizie:", error);
    return { success: false, message: "Błąd połączenia z serwerem" };
  }
};