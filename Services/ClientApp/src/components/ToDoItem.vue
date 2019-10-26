<template>
    <div>
        <b-field>
            <b-input :value="todo.text" readonly="true" />
            <b-button type="is-danger" @click="deleteTodo">
                <b-icon icon="close" />
            </b-button>
        </b-field>
    </div>
</template>


<script>

export default {
    props: ['todo', 'onDelete'],
    methods: {
        deleteTodo() {
            this.$emit('delete', this.todo)
        }
    }
}
</script>


<style scoped>

</style>