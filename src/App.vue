<template>
    <div id="app" class="container">
        <img alt="Vue logo" src="./assets/logo.png" width="10%">
        <b-field>
            <b-input v-model="newtodo" @ />
            <b-button type="is-success" :disabled="newtodo === ''" @click="addTodo">
                <b-icon icon="plus" />
            </b-button>
        </b-field>
        <transition-group name="fade">
            <b-field v-for="todo in todos" :key="todo.id" >
                <ToDoItem :todo="todo" @delete="deleteTodo" />
            </b-field>
        </transition-group>
    </div>
</template>

<script>
import ToDoItem from './components/ToDoItem.vue'
import { ToastProgrammatic as Toast } from 'buefy'

export default {
    name: 'app',
    data() {
        return { 
            newtodo: "",
            todoid: 0,
            todos: [ ]
        }
    },
    components: {
        ToDoItem
    },
    methods: {
        addTodo() {
            const currtodo = { text: this.newtodo, id: this.todoid }
            this.todoid = this.todoid + 1
            this.newtodo = ""

            this.todos.push(currtodo)
            Toast.open({message: 'Todo added !', type: 'is-success', position: 'is-bottom'})
        },

        deleteTodo(todo) {
            this.todos = this.todos.filter(item => item !== todo)
            Toast.open({message: 'Todo deleted !', type: 'is-danger', position: 'is-bottom'})
        }
    }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

</style>
