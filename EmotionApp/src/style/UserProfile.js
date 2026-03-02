import { StyleSheet } from 'react-native';

export default StyleSheet.create({
    navigationContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: 10,
        backgroundColor: '#f0f0f0',
        bottom: 0,
        width: '100%',
    },
    navButton: {
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
    },
    addContainer: {
        width: '100%',
        alignItems: 'center',
        justifyContent: 'center',
        margin: 20,
    },
    addButton: {
        width: 60,  
        height: 60,
        borderRadius: 30,
        backgroundColor: '#D9D9D9',
        justifyContent: 'center',
        alignItems: 'center',
    }
});