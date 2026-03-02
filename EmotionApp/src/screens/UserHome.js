import React, { useState, useContext, useEffect } from 'react';
import { View, TextInput, Button, Text, Alert, FlatList, TouchableOpacity, Modal, ScrollView } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { AuthContext } from '../context/AuthContext';
import { MaterialIcons } from '@expo/vector-icons';

import styles from '../style/UserHome.styles';

export default function UserHome() {
  const navigation = useNavigation();
  const { onAddNote } = useContext(AuthContext);
  const { onFetchNotes } = useContext(AuthContext);
  const { onAnalyzeNotes } = useContext(AuthContext);
  const [selectedNotes, setSelectedNotes] = useState([]);
  const [notes, setNotes] = useState([]);
  const [ modalNewNoteVisible, setModalNewNoteVisible] = useState(false);
  const [newNoteContent, setNewNoteContent] = useState('');
  const [selectedNote, setSelectedNote] = useState(null);
const [modalViewVisible, setModalViewVisible] = useState(false);

  useEffect(() => {
      const loadNotes = async () => {
        const userNotes = await onFetchNotes();
        setNotes(userNotes);
      };
      loadNotes();
    }, []);


  const toggleSelection = (id) => {
    if (selectedNotes.includes(id)) {
      setSelectedNotes(selectedNotes.filter(noteId => noteId !== id));
    } else {
      setSelectedNotes([...selectedNotes, id]);
    }
  };

  const handleSaveNote = async () => {
  if (newNoteContent.trim().length > 0) {
    await onAddNote(newNoteContent);
    setNewNoteContent('');
    setModalNewNoteVisible(false);
  }
};

const handleOpenNote = (note) => {
  setSelectedNote(note);
  setModalViewVisible(true);
};

const handleSendSelected = async () => {
  const result = await onAnalyzeNotes(selectedNotes);
  
  if (result.success) {
    alert("Analiza AI zakończona!");
    setSelectedNotes([]);
    const updatedNotes = await onFetchNotes();
    setNotes(updatedNotes); 
  } else {
    alert("Coś poszło nie tak: " + result.message);
  }
};

const renderNote = ({ item }) => {
  const isSelected = selectedNotes.includes(item.id);

  return (
    <TouchableOpacity 
      style={[
        styles.noteCard, 
        isSelected && styles.noteCardSelected
      ]}
      onLongPress={() => toggleSelection(item.id)} 
      onPress={() => {
        if (selectedNotes.length > 0) {
          toggleSelection(item.id);
        } else {
          handleOpenNote(item);
        }
      }}
    >
      <View style={styles.noteHeader}>
        {isSelected && <MaterialIcons name="check-circle" size={18} color="#4A90E2" />}
      </View>
      <View style={styles.notePreview}>
        <Text style={styles.noteTextPreview} numberOfLines={2}>
        {item.content}
      </Text>
        <Text style={styles.noteDate}>
          {new Date(item.createdAt).toLocaleDateString()}
        </Text>
      </View>
    </TouchableOpacity>
  );
};

    return (
  <View style={styles.homeContainer}>
  {selectedNotes.length > 0 && (
    <TouchableOpacity 
      style={[styles.actionButton, styles.sendButton]} 
      onPress={handleSendSelected}
    >
      <MaterialIcons name="psychology" size={30} color="white" />
      <Text style={styles.buttonBadge}>{selectedNotes.length}</Text>
    </TouchableOpacity>
  )}
      <TouchableOpacity 
        style={styles.addButton}
        onPress={() => setModalNewNoteVisible(true)}
      >
        <MaterialIcons name="add" size={30} color="grey" />

      </TouchableOpacity>

      
    <View style={styles.notesContainer}>
      <FlatList
        data={notes}
        keyExtractor={(item) => item.id}
        renderItem={renderNote}
        ListEmptyComponent={
          <Text style={styles.empty}>Brak notatek. Dodaj swoją pierwszą!</Text>
        }
      />
    </View>

    <Modal
      animationType="slide"
      transparent={true}
      visible={modalNewNoteVisible}
      onRequestClose={() => setModalNewNoteVisible(false)}
    >
      <View style={styles.modalContainer}>
        <View style={styles.modalContent}>
          <View style={styles.modalButtons}>
            <Button title="Anuluj" color="red" onPress={() => setModalNewNoteVisible(false)} />
            <Button title="Zapisz" onPress={handleSaveNote} /> 
          </View>
          <Text style={styles.modalTitle}>Dodaj nową notatkę</Text>

          <TextInput
            style={styles.textInput}
            placeholder="Zacznij pisać..."
            placeholderTextColor="#999"
            multiline
            underlineColorAndroid="transparent"
            value={newNoteContent}
            onChangeText={setNewNoteContent}
          />
          
        </View>
      </View>
    </Modal>

    <Modal
      animationType="fade"
      transparent={true}
      visible={modalViewVisible}
      onRequestClose={() => setModalViewVisible(false)}
    >
      <View style={styles.modalContainer}>
        <View style={styles.modalContent}>
          <Text style={styles.modalTitle}>Twoja Notatka</Text>
          <ScrollView style={styles.scrollNote}>
            <Text style={styles.fullNoteText}>{selectedNote?.content}</Text>
          </ScrollView>
          <Button title="Zamknij" onPress={() => setModalViewVisible(false)} />
        </View>
      </View>
    </Modal>

    <View style={styles.navigationContainer}>
      <TouchableOpacity style={styles.navButton}>
        <MaterialIcons name="home" size={30} color="#4A90E2" />
        <Text style={{color: '#4A90E2'}}>Home</Text>
      </TouchableOpacity>
      
      <TouchableOpacity 
        style={styles.navButton} 
        onPress={() => navigation.navigate('UserProfile')}
      >
        <MaterialIcons name="person" size={30} color="grey" />
        <Text>Profile</Text>
      </TouchableOpacity>
    </View>
  </View>
);
}